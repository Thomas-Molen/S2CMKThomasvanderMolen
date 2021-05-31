<?php

namespace App\Http\Controllers;

use App\Helpers\QueryHelper;
use App\Helpers\RunHelper;
use App\Helpers\TableReadabilityHelper;
use App\Models\Run;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Http\Request;

class PersonalRunsController extends Controller
{
    public function index(TableReadabilityHelper $readabilityHelper, QueryHelper $query)
    {
        return view('pages.personal_runs')
            ->with(['runs' => $query->GetUserRun(), 'readabilityHelper' => $readabilityHelper]);
    }
}
