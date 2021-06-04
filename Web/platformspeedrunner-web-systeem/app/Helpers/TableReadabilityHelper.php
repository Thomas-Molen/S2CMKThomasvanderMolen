<?php

namespace App\Helpers;

use App\Models\Role;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

class TableReadabilityHelper
{
    public function ShortenString($string, $maxLength)
    {
        $shortContent = $string;
        if (strlen($string) > $maxLength)
        {
            $shortContent = substr($string, 0, $maxLength) . "...";
        }
        return $shortContent;
    }

    public function AddShortContent($comment)
    {
        $shortContent = $comment->content;
        if (strlen($comment->content) > 20)
        {
            $shortContent = substr($comment->content, 0, 20) . "...";
        }
        $comment->{"shortContent"} = $shortContent;
    }

    public function FormatTime($ms)
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
}
