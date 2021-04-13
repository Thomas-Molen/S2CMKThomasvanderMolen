<?php

namespace App\Http\Controllers\Auth;

use App\Http\Controllers\Controller;
use App\Models\User;
use Carbon\Carbon;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\DB;

class LoginController extends Controller
{
    public function index()
    {
        return view('auth.login');
    }

    public function store(Request $request)
    {
        $this->validate($request, [
            'username' => 'required',
            'password' => 'required'
        ]);

        $user = DB::table('user')
            ->where('active', '=', 1)
            ->get();

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
