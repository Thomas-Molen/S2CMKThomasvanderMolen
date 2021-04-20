<?php

namespace App\Http\Controllers;

use App\Models\Comment;
use App\Models\Run;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Http\Request;

class PersonalCommentsController extends Controller
{
    public function index()
    {
        $comments = Comment::paginate();

        return view('pages.personal_comments', compact('comments'))
            ->with('i', (request()->input('page', 1) - 1) * $comments->perPage());
    }

    public function GetComments($comments)
    {
        $array = [];
        foreach ($comments as $comment)
        {
            if ($comment->user_id === auth()->id())
            {
                array_push($array, $comment);
            }
        }
        return $array;
    }
}
