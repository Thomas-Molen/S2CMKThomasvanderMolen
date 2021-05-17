<?php

namespace App\Helpers;

use App\Http\Controllers\RoleController;
use App\Models\Role;
use App\Models\Run;
use App\Models\User;
use Illuminate\Http\Request;

class AuthenticationHelper
{
    public function AuthAccess()
    {
        if($this->IsAdmin())
        {
            return true;
        }
        else
        {
            abort(403, 'Unauthorized action');
        }
    }

    public function IsAdmin()
    {
        if (auth()->user())
        {
            if (auth()->user()->role_id === (new RoleController)->CheckRoleByName("admin"))
            {
                return true;
            }
        }
        return false;
    }

    public function IsCurrentUser($id)
    {
        if ($this->IsAdmin())
        {
            return true;
        }
        else if (auth()->id() === (int)$id)
        {
            return true;
        }
        return false;
    }
}