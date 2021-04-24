<?php

namespace App\Http\Controllers;

use App\Helpers\AuthenticationHelper;
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
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        if ((new AuthenticationHelper)->AuthAccess()) {
            $comments = Comment::paginate();

            return view('comment.index', compact('comments'))
                ->with('i', (request()->input('page', 1) - 1) * $comments->perPage());
        }
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        return redirect()->route('leaderboard')
            ->with('error', 'The selected path was inaccessible');
    }

    /**
     * Show the form for creating a new resource as a normal user.
     *
     * @return \Illuminate\Http\RedirectResponse
     */
    public function run_create(Request $request, int $id)
    {
        if (Run::find($id) !== null) {
            $comment = new Comment();
            return view('comment.create')->with(['comment' => $comment, 'run_id' => $id]);
        }
        return redirect()->route('leaderboard')
            ->with('error', 'The selected path was inaccessible');
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request $request
     * @return \Illuminate\Http\RedirectResponse
     */
    public function store(Request $request)
    {
        request()->validate(Comment::$rules);
        $comment = Comment::create($request->all());
        if (empty($request->user_id))
        {
            $comment->update(['user_id' => auth()->id()]);
        }
        if (empty($request->run_id))
        {
            $comment->update(['run_id' => $request->run_id]);
        }
        $comment->update(['created_at' => date('Y-m-d H:i:s')]);

        return redirect()->route('run.show', $request->run_id)
            ->with('success', 'Comment created successfully.');
    }

    /**
     * Display the specified resource.
     *
     * @param  int $id
     * @return \Illuminate\Http\Response
     */
    public function show($id)
    {
        if ((new AuthenticationHelper)->AuthAccess()) {
            $comment = Comment::find($id);

            return view('comment.show', compact('comment'));
        }
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int $id
     * @return \Illuminate\Http\Response
     */
    public function edit($id)
    {
        $comment = Comment::find($id);
        if ($comment !== null AND $comment->active === 1 AND (new AuthenticationHelper)->IsCurrentUser($comment->user_id)) {
            return view('comment.edit', compact('comment'));
        }
        return redirect()->route('leaderboard')
            ->with('error', 'The comment you where trying to find has been deleted');
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request $request
     * @param  Comment $comment
     * @return \Illuminate\Http\RedirectResponse
     */
    public function update(Request $request, Comment $comment)
    {
        request()->validate(Comment::$rules);

        $comment->update($request->all());

        return redirect()->route('run.show', $comment->run_id)
            ->with('success', 'Comment updated successfully');
    }

    /**
     * @param int $id
     * @return \Illuminate\Http\RedirectResponse
     * @throws \Exception
     */
    public function destroy($id)
    {
        $comment = Comment::find($id)->update(['active' => 0]);

        return redirect()->route('run.show', Comment::find($id)->user_id)
            ->with('success', 'Comment deleted successfully');
    }

    public function ShowContent($content)
    {
        if (strlen($content) > 20)
        {
            return substr($content, 0, 20) . "...";
        }
        return $content;
    }

    public function GetCommentsByRunId($id)
    {
        $array = [];
        foreach (Comment::all() as $comment)
        {
            if ($comment->run_id === (int)$id)
            {
                if ($comment->active === 1)
                {
                    array_push($array, $comment);
                }
            }
        }
        return $array;
    }
}
