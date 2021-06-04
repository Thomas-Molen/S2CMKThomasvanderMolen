<?php

namespace App\Http\Controllers;


use App\Helpers\AuthenticationHelper;
use App\Repository\Repository;
use App\Helpers\TableReadabilityHelper;
use App\Models\Link;
use App\Models\Run;
use Illuminate\Http\Request;

/**
 * Class LinkController
 * @package App\Http\Controllers
 */
class LinkController extends Controller
{
    private $query;
    private $authenticator;

    public function __construct(Repository $queryHelper, AuthenticationHelper $authenticationHelper)
    {
        $this->query = $queryHelper;
        $this->authenticator = $authenticationHelper;
    }

    public function index(TableReadabilityHelper $readabilityHelper)
    {
        if ($this->authenticator->AuthAccess()) {

            return view('link.index')
                ->with(['links' => $this->query->Get(Link::class, false), 'readabilityHelper' => $readabilityHelper]);
        }
    }

    public function create()
    {
        return redirect()->route('leaderboard')
            ->with('error', 'The selected path was inaccessible');
    }

    public function run_create(int $id)
    {
        $run = $this->query->Find(Run::class, $id);
        if ($run !== null AND $this->authenticator->IsCurrentUser($run->user_id)) {
            return view('link.create')
                ->with(['link' => $this->query->Create(Link::class), 'run_id' => $id]);
        }
        return redirect()->route('leaderboard')
            ->with('error', 'The selected path was inaccessible');
    }

    public function store(Request $request)
    {
        $this->query->Create(Link::class, $request);

        return redirect()->route('run.show', $request->run_id)
            ->with('success', 'Link created successfully.');
    }

    public function show($id)
    {
        if ($this->authenticator->AuthAccess()) {
            return view('link.show')
                ->with(['link' => $this->query->Find(Link::class, $id)]);
        }
    }

    public function edit($id)
    {
        $link = $this->query->Find(Link::class, $id);
        if ($link !== null AND $this->authenticator->IsCurrentUser($link->user_id)) {
            return view('link.edit')
                ->with(['link' => $link]);
        }
        return redirect()->route('leaderboard')
            ->with('error', 'The link you where trying to find has been deleted');
    }

    public function update(Request $request, Link $link)
    {
        $this->query->Update(Link::class, $link, $request);

        return redirect()->route('run.show', $link->run_id)
            ->with('success', 'Link updated successfully');
    }

    public function destroy($id)
    {
        $run_id = $this->query->Find(Link::class, $id)->run_id;
        $this->query->Delete(Link::class, $id);

        return redirect()->route('run.show', $run_id)
            ->with('success', 'Link deleted successfully');
    }
}
