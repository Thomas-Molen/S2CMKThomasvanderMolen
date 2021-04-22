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

    static function SortRuns($runs)
    {
        $array = [];
        foreach ($runs as $run) {
            array_push($array, $run);
        }

        usort($array, function ($a, $b) {
            if ($a->duration == $b->duration) {
                return 0;
            }
            return ($a->duration < $b->duration) ? -1 : 1;
        });
        return ($array);
    }

    static function FormatTime($ms)
    {
        $value = array(
            'minutes' => 0,
            'seconds' => 0,
            'milliseconds' => $ms % 1000
        );

        $totalSeconds = ($ms / 1000);

        $time = '';

        if ($totalSeconds >= 6000) {
            $value['minutes'] = floor($totalSeconds / 60);
            $totalSeconds = $totalSeconds % 60;

            $time .= $value['minutes'] . ':';
        } else if ($totalSeconds >= 600) {
            $value['minutes'] = floor($totalSeconds / 60);
            $totalSeconds = $totalSeconds % 60;

            $time .= '0' . $value['minutes'] . ':';
        } else if ($totalSeconds >= 60) {
            $value['minutes'] = floor($totalSeconds / 60);
            $totalSeconds = $totalSeconds % 60;

            $time .= '00' . $value['minutes'] . ':';
        } else {
            $time .= '000:';
        }

        $value['seconds'] = floor($totalSeconds);

        if ($value['seconds'] < 10) {
            $value['seconds'] = '0' . $value['seconds'];
        }
        $time .= $value['seconds'] . ':';

        if ($value['milliseconds'] < 10) {
            $value['milliseconds'] = '00' . $value['milliseconds'];
        } else if ($value['milliseconds'] < 100) {
            $value['milliseconds'] = '0' . $value['milliseconds'];
        }
        $time .= $value['milliseconds'];

        return $time;
    }

    static function SetSuffix($i)
    {
        if ($i <= 20)
        {
            switch ($i) {
                case 1:
                    return " " . $i . "st";
                case 2:
                    return " " . $i . "nd";
                case 3:
                    return " " . $i . "rd";
                default:
                    return $i . "th";
            }
        }
        else
        {
            switch (substr($i, -1))
            {
                case 1:
                    return $i . "st";
                case 2:
                    return $i . "nd";
                case 3:
                    return $i . "rd";
                default:
                    return $i . "th";
            }
        }
    }
}
