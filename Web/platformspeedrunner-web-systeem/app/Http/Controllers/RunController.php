<?php

namespace App\Http\Controllers;

use App\Helpers\AuthenticationHelper;
use App\Helpers\RoutingHelper;
use App\Helpers\RunHelper;
use App\Helpers\TableReadabilityHelper;
use App\Models\Comment;
use App\Models\Link;
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
    public function index(AuthenticationHelper $authenticationHelper, TableReadabilityHelper $readabilityHelper)
    {
        if ($authenticationHelper->AuthAccess()) {
            $runs = Run::all();

            return view('run.index', compact('runs'))
                ->with(['runs' => $runs, 'readabilityHelper' => $readabilityHelper]);
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
    public function show($id, TableReadabilityHelper $readabilityHelper, RoutingHelper $routingHelper, AuthenticationHelper $authenticationHelper)
    {
        $run = Run::find($id);

        if ($run === null)
        {
            return redirect()->route('leaderboard')
                ->with('error', 'The run you where trying to find does not exist');
        }
        return view('run.show', compact('run'))
            ->with(['comments' => Comment::where('run_id', '=', $id)->get(), 'links' => Link::where('run_id', '=', $id)->get(), 'routingHelper' => $routingHelper, 'authenticationHelper' => $authenticationHelper, 'readabilityHelper' => $readabilityHelper]);
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int $id
     * @return \Illuminate\Http\Response
     */
    public function edit($id, AuthenticationHelper $authenticationHelper, RoutingHelper $routingHelper)
    {
        $run = Run::find($id);
        if ($run !== null AND ($run->active === 1 OR $authenticationHelper->IsAdmin()))
        {
            if ($authenticationHelper->IsCurrentUser($run->user_id)) {
                return view('run.edit', compact('run'))
                    ->with(['routingHelper' => $routingHelper, 'authenticationHelper' => $authenticationHelper]);
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
    public function destroy($id, AuthenticationHelper $authenticationHelper)
    {
        Run::find($id)->update(['active' => false]);
        foreach (Comment::where('run_id', '=', $id)->where('active', '=', true) as $comment)
        {
            Comment::find($comment->id)->update(['active' => false]);
        }

        if ($authenticationHelper->IsAdmin()) {
            return redirect()->route('run.index')
                ->with('success', 'Run deleted successfully');
        }
        return redirect()->route('personal_runs')
            ->with('success', 'Run deleted successfully');
    }

    public function apiCreate(Request $request)
    {
        $run = Run::create([
            'user_id' => User::where('unique_key', '=', $request->unique_key),
            'active' => 1,
            'created_at' => date('Y-m-d H:i:s'),
            'duration' => $request->duration,
            'upvotes' => 0,
            'information' => "",
            'custom_name' => ""
        ]);
        $run->update(['custom_name' => "#" . $run->id]);
    }
}
