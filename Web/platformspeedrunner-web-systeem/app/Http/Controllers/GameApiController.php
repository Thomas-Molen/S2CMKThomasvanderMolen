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
    private $query;

    public function __construct(QueryHelper $queryHelper)
    {
        $this->query = new $queryHelper();
    }

    public function SubmitRun(Request $request)
    {
        $this->query->CreateRun($request);
    }

    public function GetUsername(string $unique_key = null)
    {
        $user = $this->query->FindUserByUniqueKey($unique_key);
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
        while ($this->query->FindUserByUniqueKey($unique_key, false) !== null) {
            $unique_key = substr(str_shuffle(str_repeat($chars, ceil($length/strlen($chars)) )),1,$length);
        }
        return $unique_key;
    }

    public function GetBestTime(string $unique_key = null)
    {
        $user = $this->query->FindUserByUniqueKey($unique_key);
        if ($user === null ) {
            return "no user found with such key";
        }
        $bestTime = $this->query->FindBestUserRun($user->id);
        if ($bestTime === null)
        {
            return "no runs found from user";
        }
        return $bestTime;
    }
}
