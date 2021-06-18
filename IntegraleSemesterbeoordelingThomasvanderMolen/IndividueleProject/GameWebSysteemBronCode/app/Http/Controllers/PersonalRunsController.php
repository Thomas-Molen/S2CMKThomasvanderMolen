<?php

namespace App\Http\Controllers;

use App\Repository\Repository;
use App\Helpers\TableReadabilityHelper;
use App\Repository\RunRepository;

class PersonalRunsController extends Controller
{
    public function index(TableReadabilityHelper $readabilityHelper, RunRepository $runRepository)
    {
        return view('pages.personal_runs')
            ->with(['runs' => $runRepository->GetUserRun(), 'readabilityHelper' => $readabilityHelper]);
    }
}
