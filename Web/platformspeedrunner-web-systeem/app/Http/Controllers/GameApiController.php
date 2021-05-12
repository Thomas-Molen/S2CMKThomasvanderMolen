<?php

namespace App\Http\Controllers;

use App\Helpers\AuthenticationHelper;
use App\Models\Comment;
use App\Models\Run;
use App\Models\User;
use Illuminate\Http\Request;
use PHPUnit\Util\Json;
use function PHPUnit\Framework\isNull;

/**
 * Class GameApiController
 * @package App\Http\Controllers
 */
class GameApiController extends Controller
{
    public function SubmitRun(Request $request)
    {
        request()->validate([
            'unique_key' => 'required|max:20',
            'duration' => 'required'
        ]);

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

    public function GetUserData()
    {
        $return_value = "HELLO FROM THE SERVER I GOT YOU!";
        return $return_value;
//        return User::where('unique_key', '=', $request->unique_key)->get()[0];
    }
}
