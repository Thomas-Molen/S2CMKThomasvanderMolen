<?php

namespace App\Http\Controllers;

use App\Helpers\AuthenticationHelper;
use App\Repository\CommentRepository;
use App\Repository\Repository;
use App\Helpers\TableReadabilityHelper;
use App\Models\Run;
use Illuminate\Http\Request;

/**
 * Class RunController
 * @package App\Http\Controllers
 */
class RunController extends Controller
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

            return view('run.index')
                ->with(['runs' => $this->query->Get(Run::class, false), 'readabilityHelper' => $readabilityHelper]);
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

    public function show($id, TableReadabilityHelper $readabilityHelper, CommentRepository $commentRepository)
    {
        $run = $this->query->Find(Run::class, $id);

        if ($run === null)
        {
            return redirect()->route('leaderboard')
                ->with('error', 'The run you where trying to find does not exist');
        }
        return view('run.show')
            ->with(['run' => $run, 'comments' => $commentRepository->GetRunComment($run->id), 'links' => $run->link()->get(),
                    'authenticationHelper' => $this->authenticator, 'readabilityHelper' => $readabilityHelper]);
    }

    public function edit($id)
    {
        $run = $this->query->Find(Run::class, $id);
        if ($run !== null AND ($run->active === 1 OR auth()->user()->admin))
        {
            if ($this->authenticator->IsCurrentUser($run->user_id)) {
                return view('run.edit')
                    ->with(['run' => $run, 'authenticationHelper' => $this->authenticator]);
            }
        }
        return redirect()->route('leaderboard')
            ->with('error', 'The run you where trying to find has been deleted');
    }

    public function update(Request $request, Run $run)
    {
        $this->query->Update(Run::class, $run, $request);

        return redirect()->route('run.show', $run->id)
            ->with('success', 'Run updated successfully');
    }

    public function destroy($id)
    {
        $this->query->Delete(Run::class, $id);

        return redirect()->route('leaderboard')
            ->with('success', 'Run deleted successfully');
    }
}
