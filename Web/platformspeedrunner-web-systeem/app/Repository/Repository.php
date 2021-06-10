<?php

namespace App\Repository;

use App\Models\Link;
use Illuminate\Http\Request;

class Repository
{
    public function Get($model, $onlyActive = true)
    {
        if ($onlyActive) {
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
        ($model !== Link::class) ? $this->Find($model, $id)->update(['active' => false]) : $this->Find($model, $id)->delete();
    }

    public function Create($model, Request $request = null)
    {
        if (empty($request)) {
            return new $model;
        }
        request()->validate($model::$rules);
        return $model::create($request->all());
    }

    public function Update($model, $object, Request $request)
    {
        request()->validate($model::$rules);
        $object->update($request->all());
    }
}
