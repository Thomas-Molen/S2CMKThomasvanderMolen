<?php

namespace App\Http\Controllers;

use App\Helpers\AuthenticationHelper;
use App\Repository\Repository;
use App\Models\User;
use App\Repository\UserRepository;
use Illuminate\Http\Request;

/**
 * Class UserController
 * @package App\Http\Controllers
 */
class UserController extends Controller
{
    private $query;
    private $authenticator;

    public function __construct(Repository $queryHelper, AuthenticationHelper $authenticationHelper)
    {
        $this->query = $queryHelper;
        $this->authenticator = $authenticationHelper;
    }

    public function index()
    {
        if ($this->authenticator->AuthAccess()) {
            return view('user.index')
                ->with(['users' => $this->query->Get(User::class, false)]);
        }
    }

    public function create()
    {
        if ($this->authenticator->AuthAccess()) {
            return view('user.create')
                ->with(['user' => $this->query->Create(User::class)]);
        }
    }

    public function store(Request $request, UserRepository $userRepository)
    {
        $userRepository->Create($request);

        return redirect()->route('user.index')
            ->with('success', 'User created successfully.');
    }

    public function show($id)
    {
        if ($this->authenticator->AuthAccess()) {
            return view('user.show')
                ->with(['user' => $this->query->Find(User::class, $id)]);
        }
    }

    public function edit($id)
    {
        if ($this->authenticator->AuthAccess()) {
            return view('user.edit')
                ->with(['user' => $this->query->Find(User::class, $id)]);
        }
        return redirect()->route('leaderboard')
            ->with('error', 'The path you where trying to reach is inaccessible');
    }

    public function update(Request $request, User $user)
    {
        $this->query->Update(User::class, $user, $request);

        return redirect()->route('user.index')
            ->with('success', 'User updated successfully');
    }

    public function destroy($id)
    {
        $this->query->Delete(User::class, $id);

        return redirect()->route('user.index')
            ->with('success', 'User deleted successfully');
    }
}
