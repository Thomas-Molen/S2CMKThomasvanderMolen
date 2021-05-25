<?php

namespace App\Http\Controllers;

use App\Helpers\RunHelper;
use App\Helpers\TableReadabilityHelper;
use App\Models\Run;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Http\Request;

class PersonalRunsController extends Controller
{
    public function index(TableReadabilityHelper $readabilityHelper)
    {
        $runs = Run::where('active', '=', true)->where('user_id', '=', auth()->id())->get();

        return view('pages.personal_runs', compact('runs'))
            ->with(['runs' => $runs, 'readabilityHelper' => $readabilityHelper]);
    }
}
