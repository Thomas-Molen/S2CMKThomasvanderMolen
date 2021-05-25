<?php

namespace App\Http\Controllers;


use App\Helpers\AuthenticationHelper;
use App\Helpers\TableReadabilityHelper;
use App\Models\Link;
use App\Models\Run;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;

/**
 * Class LinkController
 * @package App\Http\Controllers
 */
class LinkController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index(TableReadabilityHelper $readabilityHelper, AuthenticationHelper $authenticationHelper)
    {
        if ((new AuthenticationHelper)->AuthAccess()) {
            $links = Link::all();

            return view('link.index', compact('links'))
                ->with(['links' => $links, 'readabilityHelper' => $readabilityHelper]);
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
     * @return \Illuminate\Http\Response
     */
    public function run_create(Request $request, int $id)
    {
        if (Run::find($id) !== null AND (new AuthenticationHelper)->IsCurrentUser(Run::find($id)->user_id)) {
            $link = new Link();
            return view('link.create')->with(['link' => $link, 'run_id' => $id]);
        }
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
        request()->validate(Link::$rules);

        $link = Link::create($request->all());
        if (empty($request->user_id))
        {
            $link->update(['user_id' => auth()->id()]);
        }
        if (empty($request->run_id))
        {
            $link->update(['run_id' => $request->run_id]);
        }
        if ($request->name === null OR $request->name === "")
        {
            $link->update(['name' => $request->url]);
        }

        return redirect()->route('run.show', $request->run_id)
            ->with('success', 'Link created successfully.');
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
            $link = Link::find($id);

            return view('link.show', compact('link'));
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
        $link = Link::find($id);
        if ($link !== null AND(new AuthenticationHelper)->IsCurrentUser($link->user_id)) {

            return view('link.edit', compact('link'));
        }
        return redirect()->route('leaderboard')
            ->with('error', 'The link you where trying to find has been deleted');
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request $request
     * @param  Link $link
     * @return \Illuminate\Http\Response
     */
    public function update(Request $request, Link $link)
    {
        request()->validate(Link::$rules);

        $link->update($request->all());
        if ($request->name === null OR $request->name === "")
        {
            $link->update(['name' => $request->url]);
        }

        return redirect()->route('run.show', $link->run_id)
            ->with('success', 'Link updated successfully');
    }

    /**
     * @param int $id
     * @return \Illuminate\Http\RedirectResponse
     * @throws \Exception
     */
    public function destroy($id)
    {
        $link = Link::find($id);
        $run_id = $link->run_id;
        $link->delete();

        return redirect()->route('run.show', $run_id)
            ->with('success', 'Link deleted successfully');
    }
    public function GetLinksByRunId($id)
    {
        $array = [];
        foreach (Link::all() as $link)
        {
            if ($link->run_id === (int)$id)
            {
                array_push($array, $link);
            }
        }
        return $array;
    }

}
