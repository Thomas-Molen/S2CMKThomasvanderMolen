<?php


namespace App\Repository;


use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;

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

    public function Create(Request $request)
    {
        request()->validate(User::$rules);
        return User::create([
            'username' => $request->username,
            'password' => Hash::make($request->password),
            'unique_key' => $request->unique_key
        ]);
    }
}
