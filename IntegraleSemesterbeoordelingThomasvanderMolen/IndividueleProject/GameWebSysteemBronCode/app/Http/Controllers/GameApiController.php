<?php

namespace App\Http\Controllers;


use App\Models\Run;
use App\Repository\Repository;
use App\Repository\RunRepository;
use App\Repository\UserRepository;
use Illuminate\Http\Request;

/**
 * Class GameApiController
 * @package App\Http\Controllers
 */
class GameApiController extends Controller
{
    private $userRepository;

    public function __construct(UserRepository $queryHelper)
    {
        $this->userRepository = $queryHelper;
    }

    public function SubmitRun(Request $request, RunRepository $runrepository)
    {
        $runrepository->Create($request);
    }

    public function GetUsername(string $unique_key = null)
    {
        $user = $this->userRepository->FindUserByUniqueKey($unique_key);
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
        while ($this->userRepository->FindUserByUniqueKey($unique_key, false) !== null) {
            $unique_key = substr(str_shuffle(str_repeat($chars, ceil($length/strlen($chars)) )),1,$length);
        }
        return $unique_key;
    }

    public function GetBestTime(string $unique_key = null, RunRepository $runRepository)
    {
        $user = $this->userRepository->FindUserByUniqueKey($unique_key);
        if ($user === null ) {
            return "no user found with such key";
        }
        $bestTime = $runRepository->FindBestUserRun($user->id);
        if ($bestTime === null)
        {
            return "no runs found from user";
        }
        return $bestTime;
    }
}
