<?php

namespace App\Http\Controllers\Auth;

use App\Http\Controllers\Controller;
use App\Http\Controllers\RoleController;
use App\Models\Role;
use App\Models\Run;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;

class RegisterController extends Controller
{
    public function index(string $unique_key = "")
    {
        return view('auth.register')->with(['unique_key' => $unique_key]);
    }

    public function store(Request $request)
    {
        $this->validate($request, [
            'username' => 'required|max:30',
            'password' => 'required|confirmed',
            'unique_key' => 'required|max:20|unique:user,unique_key',
        ]);

        User::create([
            'username' => $request->username,
            'password' => Hash::make($request->password),
            'unique_key' => $request->unique_key,
            'role_id' => (new RoleController)->CheckRoleByName("user")
        ]);

        return redirect()->route('login');
    }
}
