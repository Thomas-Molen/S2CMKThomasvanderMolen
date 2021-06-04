<?php

namespace App\Helpers;

class AuthenticationHelper
{
    public function AuthAccess()
    {
        if(auth()->user()->admin)
        {
            return true;
        }
        else
        {
            abort(403, 'Unauthorized action');
        }
    }

    public function IsCurrentUser($id)
    {
        if (auth()->id() === (int)$id OR auth()->user()->admin)
        {
            return true;
        }
        return false;
    }
}
