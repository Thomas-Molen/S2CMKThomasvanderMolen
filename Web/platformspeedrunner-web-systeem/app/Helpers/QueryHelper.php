<?php

namespace App\Helpers;

use App\Models\Comment;
use App\Models\Link;
use App\Models\Role;
use App\Models\Run;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;

class QueryHelper
{
    //Comment
    public function GetComment($onlyActive = true)
    {
        if ($onlyActive)
        {
            return Comment::where('active', '=', true)->get();
        }
        return Comment::all();
    }

    public function GetRunComment($run_id)
    {
        return Comment::where('active', '=', true)->where('run_id', '=', $run_id)->get();
    }

    public function GetUserComment()
    {
        return Comment::where('active', '=', true)->where('user_id', '=', auth()->user()->id)->get();
    }

    public function FindComment($id)
    {
        return Comment::find((int)$id);
    }

    public function CreateComment(Request $request = null)
    {
        if (empty($request))
        {
            return new Comment();
        }
        request()->validate(Run::$rules);
        return Comment::create($request->all());
    }

    public function StoreComment(Request $request)
    {
        $comment = $this->CreateComment($request);
        if (empty($request->user_id))
        {
            $comment->update(['user_id' => auth()->id()]);
        }
        if (empty($request->run_id))
        {
            $comment->update(['run_id' => $request->run_id]);
        }
        $comment->update(['created_at' => date('Y-m-d H:i:s')]);
    }

    public function UpdateComment(Request $request, Comment $comment)
    {
        request()->validate(Comment::$rules);

        $comment->update($request->all());
    }

    public function DeleteComment($id)
    {
        $this->FindComment($id)->update(['active' => false]);
    }

    //Run
    public function GetRun($onlyActive = true)
    {
        if ($onlyActive)
        {
            return Run::where('active', '=', true)->get();
        }
        return Run::all();
    }

    public function GetUserRun()
    {
        return Run::where('active', '=', true)->where('user_id', '=', auth()->user()->id)->get();
    }

    public function FindRun($id)
    {
        return Run::find((int)$id);
    }

    public function FindBestUserRun($user_id)
    {
        return Run::where('user_id', '=', $user_id)->where('active', '=', true)->min('duration');
    }

    public function UpdateRun(Request $request, Run $run)
    {
        request()->validate(Run::$rules);

        $run->update($request->all());
        if ($request->custom_name === null OR $request->custom_name === "")
        {
            $run->update(['custom_name' => "#" . $run->id]);
        }
    }

    public function CreateRun(Request $request)
    {
        request()->validate(Run::$rules);
        $run = Run::create([
            'user_id' => User::where('unique_key', '=', $request->unique_key)->first()->id,
            'active' => 1,
            'created_at' => date('Y-m-d H:i:s'),
            'duration' => $request->duration,
            'information' => "",
            'custom_name' => ""
        ]);
        $run->update(['custom_name' => "#" . $run->id]);
    }

    public function DeleteRun($id)
    {
        $this->FindRun($id)->update(['active' => false]);
        foreach ($this->GetRunComment($id) as $comment)
        {
            $this->DeleteComment($comment->id);
        }
        foreach ($this->GetRunLink($id) as $link)
        {
            $this->DeleteLink($link->id);
        }
    }

    //Link
    public function GetLink()
    {
        return Link::all();
    }

    public function FindLink($id)
    {
        return Link::find((int)$id);
    }

    public function GetRunLink($run_id)
    {
        return Link::where('run_id', '=', $run_id)->get();
    }

    public function CreateLink(Request $request = null)
    {
        if (empty($request))
        {
            return new Link();
        }
        request()->validate(Link::$rules);
        return Link::create($request->all());
    }

    public function StoreLink(Request $request)
    {
        $link = $this->CreateLink($request);
        if (empty($request->user_id))
        {
            $link->update(['user_id' => auth()->id()]);
        }
        if (empty($request->run_id))
        {
            $link->update(['run_id' => $request->run_id]);
        }
        if ($request->name === null OR $request->name === "")
        {
            $link->update(['name' => $request->url]);
        }
    }

    public function UpdateLink(Request $request, Link $link)
    {
        request()->validate(Link::$rules);

        $link->update($request->all());
        if ($request->name === null OR $request->name === "")
        {
            $link->update(['name' => $request->url]);
        }
    }

    public function DeleteLink($id)
    {
        $this->FindLink($id)->delete();
    }

    //Role
    public function GetRole($onlyActive = true)
    {
        if ($onlyActive)
        {
            return Role::where('active', '=', true)->get();
        }
        return Role::all();
    }

    public function FindRole($id)
    {
        return Role::find((int)$id);
    }

    public function UpdateRole(Request $request, Role $role)
    {
        request()->validate(Role::$rules);
        $role->update($request->all());
    }

    public function CreateRole(Request $request = null)
    {
        if (empty($request))
        {
            return new Role();
        }
        request()->validate(Role::$rules);
        return Role::create($request->all());
    }

    public function DeleteRole($id)
    {
        $this->FindRole($id)->update(['active' => false]);
    }

    public function GetRoleByName($name)
    {
        $role = Role::where('name', '=', $name)->where('active', '=', true)->first();
        if ($role !== null)
        {
            return $role;
        }
        switch ($name)
        {
            case 'admin':
                $role = Role::create([
                    'name' => $name,
                    'description' => "Role for administrators (Full Access)"
                ]);
                break;
            case 'user':
                $role = Role::create([
                    'name' => $name,
                    'description' => "Role for default members"
                ]);
                break;
            default:
                $role = Role::create([
                    'name' => $name
                ]);
                break;
        }
        return $role;
    }

    //User
    public function GetUser($onlyActive = true)
    {
        if ($onlyActive)
        {
            return User::where('active', '=', true)->get();
        }
        return User::all();
    }

    public function FindUser($id)
    {
        return User::find((int)$id);
    }

    public function FindUserByUniqueKey($unique_key, $onlyActive = true)
    {
        if ($onlyActive)
        {
            return User::where('unique_key', '=', $unique_key)->where('active', '=', true)->first();
        }
        return User::where('unique_key', '=', $unique_key)->first();
    }

    public function UpdateUser(Request $request, User $user)
    {
        request()->validate(User::$rules);

        $user->update($request->all());
    }

    public function CreateUser(Request $request = null)
    {
        if (empty($request))
        {
            return new User();
        }
        request()->validate(User::$rules);
        return User::create($request->all());
    }

    public function StoreUser(Request $request)
    {
        request()->validate(User::$rules);
        return User::create([
            'username' => $request->username,
            'password' => Hash::make($request->password),
            'unique_key' => $request->unique_key,
            'role' => $request->role_id
        ]);
    }

    public function DeleteUser($id)
    {
        $this->FindUser($id)->update(['active' => 0]);
    }
}
