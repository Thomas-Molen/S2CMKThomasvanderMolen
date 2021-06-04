<?php


namespace App\Repository;


use App\Models\User;

class UserRepository
{
    public function FindUserByUniqueKey($unique_key, $onlyActive = true)
    {
        if ($onlyActive)
        {
            return User::where('unique_key', '=', $unique_key)->where('active', '=', true)->first();
        }
        return User::where('unique_key', '=', $unique_key)->first();
    }
}
