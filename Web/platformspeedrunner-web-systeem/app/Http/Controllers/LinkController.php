<?php

namespace App\Http\Controllers;


use App\Helpers\AuthenticationHelper;
use App\Helpers\QueryHelper;
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

            return view('link.index')
                ->with(['links' => $this->query->GetLink(), 'readabilityHelper' => $readabilityHelper]);
        }
    }

    public function create()
    {
        return redirect()->route('leaderboard')
            ->with('error', 'The selected path was inaccessible');
    }

    public function run_create(Request $request, int $id)
    {
        $run = $this->query->FindRun($id);
        if ($run !== null AND $this->authenticator->IsCurrentUser($run->user_id)) {
            return view('link.create')
                ->with(['link' => $this->query->CreateLink(), 'run_id' => $id]);
        }
        return redirect()->route('leaderboard')
            ->with('error', 'The selected path was inaccessible');
    }

    public function store(Request $request)
    {
        $this->query->StoreLink($request);

        return redirect()->route('run.show', $request->run_id)
            ->with('success', 'Link created successfully.');
    }

    public function show($id)
    {
        if ($this->authenticator->AuthAccess()) {
            return view('link.show')
                ->with(['link' => $this->query->FindLink($id)]);
        }
    }

    public function edit($id)
    {
        $link = $this->query->FindLink($id);
        if ($link !== null AND $this->authenticator->IsCurrentUser($link->user_id)) {
            return view('link.edit')
                ->with(['link' => $link]);
        }
        return redirect()->route('leaderboard')
            ->with('error', 'The link you where trying to find has been deleted');
    }

    public function update(Request $request, Link $link)
    {
        $this->query->UpdateLink($request, $link);

        return redirect()->route('run.show', $link->run_id)
            ->with('success', 'Link updated successfully');
    }

    public function destroy($id)
    {
        $run_id = $this->query->FindLink($id)->run_id;
        $this->query->DeleteLink($id);

        return redirect()->route('run.show', $run_id)
            ->with('success', 'Link deleted successfully');
    }
}
