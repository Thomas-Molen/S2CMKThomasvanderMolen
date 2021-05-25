<?php

namespace App\Http\Controllers;

use App\Helpers\TableReadabilityHelper;
use App\Models\Comment;
use App\Models\Run;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Http\Request;

class PersonalCommentsController extends Controller
{
    public function index(TableReadabilityHelper $readabilityHelper)
    {
        $comments = Comment::where('user_id', '=', auth()->id())->get();

        return view('pages.personal_comments', compact('comments'))
            ->with(['comments' => $comments, 'readabilityHelper' => $readabilityHelper]);
    }
}
