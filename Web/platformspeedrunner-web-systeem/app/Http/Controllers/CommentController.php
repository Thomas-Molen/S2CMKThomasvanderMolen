<?php

namespace App\Http\Controllers;

use App\Helpers\AuthenticationHelper;
use App\Repository\Repository;
use App\Helpers\TableReadabilityHelper;
use App\Models\Comment;
use App\Models\Run;
use Illuminate\Http\Request;

/**
 * Class CommentController
 * @package App\Http\Controllers
 */
class CommentController extends Controller
{
    private $repository;
    private $authenticator;

    public function __construct(Repository $queryHelper, AuthenticationHelper $authenticationHelper)
    {
        $this->repository = $queryHelper;
        $this->authenticator = $authenticationHelper;
    }

    public function index(TableReadabilityHelper $readabilityHelper)
    {
        if ($this->authenticator->AuthAccess()) {

            return view('comment.index')
                ->with(['comments' => $this->repository->Get(Comment::class, false), 'readabilityHelper' => $readabilityHelper]);
        }
    }

    public function create()
    {
        return redirect()->route('leaderboard')
            ->with('error', 'The selected path was inaccessible');
    }

    public function run_create(int $id)
    {
        if ($this->repository->Find(Run::class, $id) !== null) {
            $comment = $this->repository->Create(Comment::class);

            return view('comment.create')
                ->with(['comment' => $comment, 'run_id' => $id, 'authenticationHelper' => $this->authenticator]);
        }
        return redirect()->route('leaderboard')
            ->with('error', 'The selected path was inaccessible');
    }

    public function store(Request $request)
    {
        $this->repository->Create(Comment::class, $request);

        return redirect()->route('run.show', $request->run_id)
            ->with('success', 'Comment created successfully.');
    }

    public function show($id)
    {
        if ($this->authenticator->AuthAccess()) {

            return view('comment.show')
                ->with(['comment' => $this->repository->Find(Comment::class, $id)]);
        }
    }

    public function edit($id)
    {
        $comment = $this->repository->Find(Comment::class, $id);
        if ($comment !== null AND ($comment->active === 1 OR auth()->user()->admin))
        {
            if ($this->authenticator->IsCurrentUser($comment->user_id)) {
                return view('comment.edit', compact('comment'))
                    ->with(['authenticationHelper' => $this->authenticator]);
            }
        }
        return redirect()->route('leaderboard')
            ->with('error', 'Could not access comment');
    }

    public function update(Request $request, Comment $comment)
    {
        $this->repository->Update(Comment::class, $comment, $request);

        return redirect()->route('run.show', $comment->run_id)
            ->with('success', 'Comment updated successfully');
    }

    public function destroy($id)
    {
        $this->repository->Delete(Comment::class, $id);

        return redirect()->route('run.show', $this->repository->Find(Comment::class, $id)->user_id)
            ->with('success', 'Comment deleted successfully');
    }
}
