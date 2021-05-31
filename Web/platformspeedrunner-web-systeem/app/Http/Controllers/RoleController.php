<?php

namespace App\Http\Controllers;

use App\Helpers\AuthenticationHelper;
use App\Helpers\QueryHelper;
use App\Models\Role;
use Illuminate\Http\Request;

/**
 * Class RoleController
 * @package App\Http\Controllers
 */
class RoleController extends Controller
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
            return view('role.index')
                ->with(['roles' => $this->query->GetRole(false)]);
        }
    }

    public function create()
    {
        if ($this->authenticator->AuthAccess()) {
            return view('role.create')
                ->with(['role' => $this->query->CreateRole()]);
        }
    }

    public function store(Request $request)
    {
        $this->query->CreateRole($request);

        return redirect()->route('role.index')
            ->with('success', 'Role created successfully.');
    }

    public function show($id)
    {
        if ($this->authenticator->AuthAccess()) {
            return view('role.show')
                ->with(['role' => $this->query->FindRole($id)]);
        }
    }

    public function edit($id)
    {
        if ($this->authenticator->AuthAccess()) {
            return view('role.edit')
                ->with(['role' => $this->query->FindRole($id)]);
        }
    }

    public function update(Request $request, Role $role)
    {
        $this->query->UpdateRole($request, $role);

        return redirect()->route('role.index')
            ->with('success', 'Role updated successfully');
    }

    public function destroy($id)
    {
        $this->query->DeleteRole($id);

        return redirect()->route('role.index')
            ->with('success', 'Role deleted successfully');
    }
}
