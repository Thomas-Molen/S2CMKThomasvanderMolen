<?php

namespace App\Http\Helpers;

use App\Models\Role;
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
            if (Role::find(auth()->user()->role_id)->name === "admin")
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
        else
        {
            if (auth()->id() === $id)
            {
                return true;
            }
        }
        return false;
    }
}
