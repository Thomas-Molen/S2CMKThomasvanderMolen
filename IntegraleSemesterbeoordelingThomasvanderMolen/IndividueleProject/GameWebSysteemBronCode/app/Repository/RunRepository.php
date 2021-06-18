<?php


namespace App\Repository;


use App\Models\Comment;
use App\Models\Link;
use App\Models\Run;
use App\Models\User;
use Illuminate\Http\Request;

class RunRepository
{
    public function GetUserRun()
    {
        return Run::where('active', '=', true)->where('user_id', '=', auth()->user()->id)->get();
    }

    public function FindBestUserRun($user_id)
    {
        return Run::where('user_id', '=', $user_id)->where('active', '=', true)->min('duration');
    }

    public function Create(Request $request)
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
        return $run->update(['custom_name' => "#" . $run->id]);
    }

    public function Update($object, Request $request)
    {
        request()->validate(Run::$rules);
        $object->update($request->all());
        if ($request->custom_name === null or $request->custom_name === "") {
            $object->update(['custom_name' => "#" . $object->id]);
        }
    }

    public function Delete($id, Repository $repository)
    {
        $run = $repository->Find(Run::class, $id);
        foreach ($run->comment()->get() as $comment) {
            $repository->Delete(Comment::class, $comment->id);
        }
        foreach ($run->link()->get() as $link) {
            $link->delete();
        }
        $repository->Delete(Run::class, $id);
    }
}
