<?php

namespace App\Http\Controllers;

use App\Helpers\AuthenticationHelper;
use App\Models\Comment;
use App\Models\Run;
use App\Models\User;
use Illuminate\Http\Request;
use function PHPUnit\Framework\isNull;

/**
 * Class RunController
 * @package App\Http\Controllers
 */
class RunController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        if ((new AuthenticationHelper)->AuthAccess()) {
            $runs = Run::paginate();

            return view('run.index', compact('runs'))
                ->with('i', (request()->input('page', 1) - 1) * $runs->perPage());
        }
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        if ((new AuthenticationHelper)->AuthAccess()) {
            $run = new Run();

            return view('run.create', compact('run'));
        }
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
        request()->validate(Run::$rules);

        $run = Run::create($request->all());
        $run->update(['created_at' => date('Y-m-d H:i:s')]);
        if ($request->custom_name === null OR $request->custom_name === "")
        {
            $run->update(['custom_name' => "#" . $run->id]);
        }

        return redirect()->route('run.show', $run->id)
            ->with('success', 'Run created successfully.');
    }

    /**
     * Display the specified resource.
     *
     * @param  int $id
     * @return \Illuminate\Http\RedirectResponse
     */
    public function show($id)
    {
        $run = Run::find($id);
        if ($run === null)
        {
            return redirect()->route('leaderboard')
                ->with('error', 'The run you where trying to find does not exist');
        }
        if ($run->active === 1)
        {
            return view('run.show', compact('run'));
        }
        return redirect()->route('leaderboard')
            ->with('error', 'The run you where trying to find has been deleted');
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int $id
     * @return \Illuminate\Http\Response
     */
    public function edit($id)
    {
        $run = Run::find($id);
        if ($run !== null AND ($run->active === 1 OR (new AuthenticationHelper)->IsAdmin()))
        {
            if ((new AuthenticationHelper)->IsCurrentUser($run->user_id)) {
                return view('run.edit', compact('run'));
            }
        }
        return redirect()->route('leaderboard')
            ->with('error', 'The run you where trying to find has been deleted');
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request $request
     * @param  Run $run
     * @return \Illuminate\Http\Response
     */
    public function update(Request $request, Run $run)
    {
        request()->validate(Run::$rules);

        $run->update($request->all());
        if ($request->custom_name === null OR $request->custom_name === "")
        {
            $run->update(['custom_name' => "#" . $run->id]);
        }

        return redirect()->route('run.show', $run->id)
            ->with('success', 'Run updated successfully');
    }

    /**
     * @param int $id
     * @return \Illuminate\Http\RedirectResponse
     * @throws \Exception
     */
    public function destroy($id)
    {
        Run::find($id)->update(['active' => 0]);
        foreach ((new CommentController)->GetCommentsByRunId($id) as $comment)
        {
            Comment::find($comment->id)->update(['active' => 0]);
        }

        if ((new AuthenticationHelper)->IsAdmin()) {
            return redirect()->route('run.index')
                ->with('success', 'Run deleted successfully');
        }
        return redirect()->route('personal_runs')
            ->with('success', 'Run deleted successfully');
    }

    public function GetName($id)
    {
        $run = Run::find($id);
        return $run->custom_name;
    }
}
