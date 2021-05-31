<?php

namespace App\Http\Controllers;

use App\Helpers\AuthenticationHelper;
use App\Helpers\QueryHelper;
use App\Helpers\TableReadabilityHelper;
use App\Models\Run;
class LeaderboardController extends Controller
{
    public function index(TableReadabilityHelper $readabilityHelper, QueryHelper $query)
    {
        $runs = $this->SortedRuns($query);

        return view('pages.leaderboard', compact('runs'))
            ->with(['runs' => $runs, 'readabilityHelper' => $readabilityHelper]);
    }

    public function SortedRuns(QueryHelper $query)
    {
        $runs = $query->GetRun();
        $leaderboardRuns = [];
        foreach ($runs as $run)
        {
            $leaderboardRuns[] = $run;
        }
        usort($leaderboardRuns,  function ($a, $b)
        {
            if ($a->duration == $b->duration)
            {
                return 0;
            }
            return ($a->duration < $b->duration) ? -1 : 1;
        });

        foreach ($leaderboardRuns as $run)
        {
            $run->{"position"} = array_search($run, $leaderboardRuns)+1;
        }
        return($leaderboardRuns);
    }
}
