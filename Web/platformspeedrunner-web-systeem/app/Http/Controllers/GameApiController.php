<?php

namespace App\Http\Controllers;

use App\Helpers\AuthenticationHelper;
use App\Helpers\QueryHelper;
use App\Models\Comment;
use App\Models\Run;
use App\Models\User;
use Illuminate\Http\Request;
use PHPUnit\Util\Json;
use function PHPUnit\Framework\isEmpty;
use function PHPUnit\Framework\isNull;

/**
 * Class GameApiController
 * @package App\Http\Controllers
 */
class GameApiController extends Controller
{
    public function SubmitRun(Request $request, QueryHelper $query)
    {
        request()->validate(Run::$rules);
        $run = Run::create([
            'user_id' => User::where('unique_key', '=', $request->unique_key)->first()->id,
            'active' => 1,
            'created_at' => date('Y-m-d H:i:s'),
            'duration' => $request->duration,
            'information' => "",
            'custom_name' => ""
        ]);
        $run->update(['custom_name' => "#" . $run->id]);
    }

    public function GetUsername(string $unique_key = null)
    {
        $user = User::where('unique_key', '=', $unique_key)->where('active', '=', true)->first();
        if ($user === null ) {
            return "no user found with such key";
        }
        return $user->username;
    }

    public function GetUniqueKey()
    {
        $length = 20;
        $chars = "0123456789ABCDEFGHIJKLMNPQRSTUVWXYZ";
        $unique_key = substr(str_shuffle(str_repeat($chars, ceil($length/strlen($chars)) )),1,$length);
        while (User::where('unique_key', '=', $unique_key)->exists()) {
            $unique_key = substr(str_shuffle(str_repeat($chars, ceil($length/strlen($chars)) )),1,$length);
        }
        return $unique_key;
    }

    public function GetBestTime(string $unique_key = null)
    {
        $user = User::where('unique_key', '=', $unique_key)->where('active', '=', true)->first();
        if ($user === null ) {
            return "no user found with such key";
        }
        $bestTime = Run::where('user_id', '=', $user->id)->where('active', '=', true)->min('duration');
        if ($bestTime === null)
        {
            return "no runs found from user";
        }
        return $bestTime;
    }
}
