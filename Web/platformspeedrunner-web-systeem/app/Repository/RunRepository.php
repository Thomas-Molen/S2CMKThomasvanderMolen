<?php


namespace App\Repository;


use App\Models\Run;

class RunRepository
{
    public function GetUserRun()
    {
        return Run::where('active', '=', true)->where('user_id', '=', auth()->user()->id)->get();
    }

    public function FindBestUserRun($user_id)
    {
        return Run::where('user_id', '=', $user_id)->where('active', '=', true)->min('duration');
    }
}
