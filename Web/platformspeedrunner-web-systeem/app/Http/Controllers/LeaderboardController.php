<?php

namespace App\Http\Controllers;

use App\Models\Run;
use Illuminate\Http\Request;
use function GuzzleHttp\Psr7\_caseless_remove;

class LeaderboardController extends Controller
{
    public function index()
    {
        $runs = Run::paginate();

        return view('pages.leaderboard', compact('runs'))
            ->with('i', (request()->input('page', 1) - 1) * $runs->perPage());
    }

    public function SortRuns()
    {
        $array = [];
        foreach (Run::paginate() as $run)
        {
            array_push($array, $run);
        }
        usort($array, function($a, $b)
        {
            return strcmp($a->duration, $b->duration);
        });
        return $array;
    }

    public function FormatTime($ms)
    {
        $value = array(
            'hours' => 0,
            'minutes' => 0,
            'seconds' => 0
        );

        $total_seconds = ($ms / 1000);

            $time = '';

            if($total_seconds >= 3600)
            {
                $value['hours'] = floor($total_seconds / 3600);
                $total_seconds = $total_seconds % 3600;

                $time .= $value['hours'] . ':';
            }

            if($total_seconds >= 60)
            {
                $value['minutes'] = floor($total_seconds / 60);
                $total_seconds = $total_seconds % 60;

                $time .= $value['minutes'] . ':';
            } else {
                $time .= '0:';
            }

            $value['seconds'] = floor($total_seconds);

            if($value['seconds'] < 10)
            {
                $value['seconds'] = '0' . $value['seconds'];
            }

            $time .= $value['seconds'] . ':';
            $time .= $ms%1000;

            return $time;
        }
    }
