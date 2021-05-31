<?php

namespace App\Http\Controllers;

use App\Helpers\AuthenticationHelper;
use App\Helpers\QueryHelper;
use App\Helpers\TableReadabilityHelper;
use App\Models\Comment;
use App\Models\User;
use App\Models\Run;
use Illuminate\Http\Request;

/**
 * Class CommentController
 * @package App\Http\Controllers
 */
class CommentController extends Controller
{
    private $query;
    private $authenticator;

    public function __construct(QueryHelper $queryHelper, AuthenticationHelper $authenticationHelper)
    {
        $this->query = $queryHelper;
        $this->authenticator = $authenticationHelper;
    }

    public function index(TableReadabilityHelper $readabilityHelper)
    {
        if ($this->authenticator->AuthAccess()) {

            return view('comment.index')
                ->with(['comments' => $this->query->GetComment(false), 'readabilityHelper' => $readabilityHelper]);
        }
    }

    public function create()
    {
        return redirect()->route('leaderboard')
            ->with('error', 'The selected path was inaccessible');
    }

    public function run_create(int $id)
    {
        if ($this->query->FindRun($id) !== null) {
            $comment = $this->query->CreateComment();

            return view('comment.create')
                ->with(['comment' => $comment, 'run_id' => $id, 'authenticationHelper' => $this->authenticator]);
        }
        return redirect()->route('leaderboard')
            ->with('error', 'The selected path was inaccessible');
    }

    public function store(Request $request)
    {
        $this->query->StoreComment($request);

        return redirect()->route('run.show', $request->run_id)
            ->with('success', 'Comment created successfully.');
    }

    public function show($id)
    {
        if ($this->authenticator->AuthAccess()) {

            return view('comment.show')
                ->with(['comment' => $this->query->FindComment($id)]);
        }
    }

    public function edit($id)
    {
        $comment = $this->query->FindComment($id);
        if ($comment !== null AND ($comment->active === 1 OR $this->authenticator->IsAdmin()))
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
        $this->query->UpdateComment($request, $comment);

        return redirect()->route('run.show', $comment->run_id)
            ->with('success', 'Comment updated successfully');
    }

    public function destroy($id)
    {
        $this->query->DeleteComment($id);

        return redirect()->route('run.show', $this->query->FindComment($id)->user_id)
            ->with('success', 'Comment deleted successfully');
    }
}
