<?php

namespace App\Http\Controllers;

use App\Helpers\AuthenticationHelper;
use App\Helpers\QueryHelper;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Hash;

/**
 * Class UserController
 * @package App\Http\Controllers
 */
class UserController extends Controller
{
    private $query;
    private $authenticator;

    public function __construct(QueryHelper $queryHelper, AuthenticationHelper $authenticationHelper)
    {
        $this->query = $queryHelper;
        $this->authenticator = $authenticationHelper;
    }

    public function index()
    {
        if ($this->authenticator->AuthAccess()) {
            return view('user.index')
                ->with(['users' => $this->query->GetUser(false)]);
        }
    }

    public function create()
    {
        if ($this->authenticator->AuthAccess()) {
            return view('user.create')
                ->with(['user' => $this->query->CreateUser()]);
        }
    }

    public function store(Request $request)
    {
        $this->query->StoreUser($request);

        return redirect()->route('user.index')
            ->with('success', 'User created successfully.');
    }

    public function show($id)
    {
        if ($this->authenticator->AuthAccess()) {
            return view('user.show')
                ->with(['user' => $this->query->FindUser($id)]);
        }
    }

    public function edit($id)
    {
        if ($this->authenticator->AuthAccess()) {
            return view('user.edit')
                ->with(['user' => $this->query->FindUser($id)]);
        }
        return redirect()->route('leaderboard')
            ->with('error', 'The path you where trying to reach is inaccessible');
    }

    public function update(Request $request, User $user)
    {
        $this->query->UpdateUser($request, $user);

        return redirect()->route('user.index')
            ->with('success', 'User updated successfully');
    }

    public function destroy($id)
    {
        $this->query->DeleteUser($id);

        return redirect()->route('user.index')
            ->with('success', 'User deleted successfully');
    }
}
