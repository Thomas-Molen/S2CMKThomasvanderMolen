<?php

namespace App\Http\Controllers\Auth;

use App\Http\Controllers\Controller;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;

class RegisterController extends Controller
{
    public function index()
    {
        return view('auth.register');
    }

    public function store(Request $request)
    {
        $this->validate($request, [
            'username' => 'required|max:30',
            'password' => 'required|confirmed',
            'unique_key' => 'required|max:20',
        ]);

        User::create([
            'username' => $request->username,
            'password' => Hash::make($request->password),
            'unique_key' => $request->unique_key,
        ]);

        return redirect()->route('login');
    }
}
