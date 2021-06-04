<?php

namespace App\Http\Controllers\Auth;

use App\Repository\Repository;
use App\Http\Controllers\Controller;
use App\Models\User;
use Illuminate\Http\Request;

class LoginController extends Controller
{
    public function index()
    {
        return view('auth.login');
    }

    public function store(Request $request, Repository $repository)
    {
        $this->validate($request, [
            'username' => 'required',
            'password' => 'required'
        ]);

        $user = $repository->Get(User::class);

        if ($user->isEmpty())
        {
            return back()->with('status', 'Your account is not active. Contact support.');
        }

        if (!auth()->attempt($request->only(['username', 'password']), $request->remember))
        {
            return back()->with('status', 'Invalid login details');
        }
        return redirect()->route('leaderboard');
    }
}
