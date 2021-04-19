<?php

namespace App\Http\Controllers;

use App\Models\Run;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Http\Request;

class PersonalRunsController extends Controller
{
    public function index()
    {
        $runs = Run::paginate();

        return view('pages.personal_runs', compact('runs'))
            ->with('i', (request()->input('page', 1) - 1) * $runs->perPage());
    }

    public function GetRuns($runs)
    {
        $array = [];
        foreach ($runs as $run)
        {
            if ($run->user_id === auth()->id())
            {
                array_push($array, $run);
            }
        }
        return $array;
    }
}
