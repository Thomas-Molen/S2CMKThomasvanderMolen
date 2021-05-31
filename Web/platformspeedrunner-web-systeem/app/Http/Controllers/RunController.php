<?php

namespace App\Http\Controllers;

use App\Helpers\AuthenticationHelper;
use App\Helpers\QueryHelper;
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

            return view('run.index')
                ->with(['runs' => $this->query->GetRun(false), 'readabilityHelper' => $readabilityHelper]);
        }
    }

    public function create()
    {
        return redirect()->route('leaderboard')
            ->with('error', 'The selected path was inaccessible');
    }

    public function store()
    {
        return redirect()->route('leaderboard')
            ->with('error', 'The selected path was inaccessible');
    }

    public function show($id, TableReadabilityHelper $readabilityHelper, RoutingHelper $routingHelper)
    {
        $run = $this->query->FindRun($id);

        if ($run === null)
        {
            return redirect()->route('leaderboard')
                ->with('error', 'The run you where trying to find does not exist');
        }
        return view('run.show')
            ->with(['run' => $run, 'comments' => $this->query->GetRunComment($id), 'links' => $this->query->GetRunLink($id), 'routingHelper' => $routingHelper,
                    'authenticationHelper' => $this->authenticator, 'readabilityHelper' => $readabilityHelper]);
    }

    public function edit($id, RoutingHelper $routingHelper)
    {
        $run = $this->query->FindRun($id);
        if ($run !== null AND ($run->active === 1 OR $this->authenticator->IsAdmin()))
        {
            if ($this->authenticator->IsCurrentUser($run->user_id)) {
                return view('run.edit')
                    ->with(['run' => $run, 'routingHelper' => $routingHelper, 'authenticationHelper' => $this->authenticator]);
            }
        }
        return redirect()->route('leaderboard')
            ->with('error', 'The run you where trying to find has been deleted');
    }

    public function update(Request $request, Run $run)
    {
        $this->query->UpdateRun($request, $run);

        return redirect()->route('run.show', $run->id)
            ->with('success', 'Run updated successfully');
    }

    public function destroy($id)
    {
        $this->query->DeleteRun($id);

        return redirect()->route('leaderboard')
            ->with('success', 'Run deleted successfully');
    }
}
