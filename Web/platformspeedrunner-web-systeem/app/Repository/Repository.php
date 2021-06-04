<?php

namespace App\Repository;

use App\Models\Comment;
use App\Models\Link;
use App\Models\Role;
use App\Models\Run;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;

class Repository
{
    public function Get($model, $onlyActive = true)
    {
        if ($onlyActive)
        {
            return $model::where('active', '=', true)->get();
        }
        return $model::all();
    }

    public function Find($model, $id)
    {
        return $model::find((int)$id);
    }

    public function Delete($model, $id)
    {
        switch ($model)
        {
            case Link::class:
                $this->Find($model, $id)->delete();
                break;

            case Run::class:
                $this->Find($model, $id)->update(['active' => false]);
                foreach (Comment::where('active', '=', true)->where('run_id', '=', $id)->get() as $comment)
                {
                    $this->Delete(Comment::class, $comment->id);
                }
                foreach (Link::where('run_id', '=', $id)->get() as $link)
                {
                    $this->Delete(Link::class, $link->id);
                }
                break;

            default:
                $this->Find($model, $id)->update(['active' => false]);
                break;
        }
    }

    public function Create($model, Request $request = null)
    {
        if (empty($request))
        {
            return new $model;
        }
        switch ($model)
        {
            case Run::class:
                request()->validate(Run::$rules);
                $run = Run::create([
                    'user_id' => User::where('unique_key', '=', $request->unique_key)->first()->id,
                    'active' => 1,
                    'created_at' => date('Y-m-d H:i:s'),
                    'duration' => $request->duration,
                    'information' => "",
                    'custom_name' => ""
                ]);
                return $run->update(['custom_name' => "#" . $run->id]);

            case User::class:
                request()->validate(User::$rules);
                return User::create([
                    'username' => $request->username,
                    'password' => Hash::make($request->password),
                    'unique_key' => $request->unique_key
                ]);

            case Link::class:
                request()->validate($model::$rules);
                return $model::create($request->all())->update(['user_id' => auth()->user()->id]);

            case Comment::class:
                request()->validate($model::$rules);
                return $model::create($request->all())->update(['user_id' => auth()->user()->id, 'created_at' => date('Y-m-d H:i:s')]);
        }
        request()->validate($model::$rules);
        return $model::create($request->all());
    }

    public function Update($model, $object, Request $request)
    {
        request()->validate($model::$rules);
        $object->update($request->all());
        if ($model === Run::class && ($request->custom_name === null OR $request->custom_name === ""))
        {
            $object->update(['custom_name' => "#" . $object->id]);
        }
    }
}
