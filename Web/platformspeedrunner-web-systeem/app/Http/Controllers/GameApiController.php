<?php

namespace App\Http\Controllers;

use App\Helpers\AuthenticationHelper;
use App\Models\Comment;
use App\Models\Run;
use App\Models\User;
use Illuminate\Http\Request;
use function PHPUnit\Framework\isNull;

/**
 * Class GameApiController
 * @package App\Http\Controllers
 */
class GameApiController extends Controller
{
    public function SubmitRun(Request $request)
    {
        request()->validate(Run::$rules);

        $run = Run::create([
            'user_id' => User::where('unique_key', '=', $request->unique_key)->get()[0]->id,
            'active' => 1,
            'created_at' => date('Y-m-d H:i:s'),
            'duration' => $request->duration,
            'upvotes' => 0,
            'information' => "",
            'custom_name' => ""
        ]);
        $run->update(['custom_name' => "#" . $run->id]);
    }

    public function GetUserData(Request $request)
    {
        return User::where('unique_key', '=', $request->unique_key)->get()[0];
    }
}
