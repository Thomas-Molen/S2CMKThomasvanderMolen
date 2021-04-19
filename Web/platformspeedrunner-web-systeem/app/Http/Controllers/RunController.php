<?php

namespace App\Http\Controllers;

use App\Models\Run;
use App\Models\User;
use Illuminate\Http\Request;

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
        if ((new AuthenticatorController)->AuthAccess()) {
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
        if ((new AuthenticatorController)->AuthAccess()) {
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

        return redirect()->route('run.index')
            ->with('success', 'Run created successfully.');
    }

    /**
     * Display the specified resource.
     *
     * @param  int $id
     * @return \Illuminate\Http\Response
     */
    public function show($id)
    {
        $run = Run::find($id);

        return view('run.show', compact('run'));
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
        if ((new AuthenticatorController)->IsCurrentUser($run->user_id)) {
            return view('run.edit', compact('run'));
        }
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

        if ((new AuthenticatorController)->IsAdmin()) {
            return redirect()->route('run.index')
                ->with('success', 'Run updated successfully');
        }
        return redirect()->route('personal_runs')
            ->with('success', 'Run updated successfully');
    }

    /**
     * @param int $id
     * @return \Illuminate\Http\RedirectResponse
     * @throws \Exception
     */
    public function destroy($id)
    {
        $run = Run::find($id)->update(['active' => 0]);

        if ((new AuthenticatorController)->IsAdmin()) {
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
