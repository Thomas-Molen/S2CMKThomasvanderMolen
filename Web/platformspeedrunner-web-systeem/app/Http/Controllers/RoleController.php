<?php

namespace App\Http\Controllers;

use App\Helpers\AuthenticationHelper;
use App\Models\Role;
use App\Models\Run;
use Illuminate\Http\Request;

/**
 * Class RoleController
 * @package App\Http\Controllers
 */
class RoleController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        if ((new AuthenticationHelper)->AuthAccess()) {
            $roles = Role::paginate();

            return view('role.index', compact('roles'))
                ->with('i', (request()->input('page', 1) - 1) * $roles->perPage());
        }
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        if ((new AuthenticationHelper)->AuthAccess()) {
            $role = new Role();
            return view('role.create', compact('role'));
        }
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
        request()->validate(Role::$rules);

        $role = Role::create($request->all());

        return redirect()->route('role.index')
            ->with('success', 'Role created successfully.');
    }

    /**
     * Display the specified resource.
     *
     * @param  int $id
     * @return \Illuminate\Http\Response
     */
    public function show($id)
    {
        if ((new AuthenticationHelper)->AuthAccess()) {
            $role = Role::find($id);

            return view('role.show', compact('role'));
        }
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int $id
     * @return \Illuminate\Http\Response
     */
    public function edit($id)
    {
        if ((new AuthenticationHelper)->AuthAccess()) {
            $role = Role::find($id);

            return view('role.edit', compact('role'));
        }
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request $request
     * @param  Role $role
     * @return \Illuminate\Http\Response
     */
    public function update(Request $request, Role $role)
    {
        request()->validate(Role::$rules);

        $role->update($request->all());

        return redirect()->route('role.index')
            ->with('success', 'Role updated successfully');
    }

    /**
     * @param int $id
     * @return \Illuminate\Http\RedirectResponse
     * @throws \Exception
     */
    public function destroy($id)
    {
        $role = Role::find($id)->update(['active' => 0]);

        return redirect()->route('role.index')
            ->with('success', 'Role deleted successfully');
    }

    public function GetName($id)
    {
        $role = Role::find($id);
        return $role->name;
    }

    public function GetIdByName($name)
    {
        foreach (Role::all() as $role)
        {
            if ($role->name === $name)
            {
                return $role->id;
            }
        }
        $run = Role::create([
            'name' => $name
        ]);
        return $run->id;
    }
}
