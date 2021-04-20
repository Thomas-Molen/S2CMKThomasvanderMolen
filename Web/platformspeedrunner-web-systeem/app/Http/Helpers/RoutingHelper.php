<?php

namespace App\Http\Helpers;

use App\Models\Role;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

class RoutingHelper
{
    public function PreviousRoute()
    {
        $route = ltrim(parse_url(url()->previous(), PHP_URL_PATH), '/');
        if (Route::has($route))
        {
            return route($route);
        }
        return $this->GetValidRoute($route);
    }

    private function GetValidRoute($route)
    {
        if (str_contains($route, 'run/'))
        {
            if (str_contains($route, 'edit') OR url()->current() === url()->previous())
            {
                return route('personal_runs');
            }
            return route('run.show', filter_var($route, FILTER_SANITIZE_NUMBER_INT));
        }
        return route('leaderboard');
    }
}
