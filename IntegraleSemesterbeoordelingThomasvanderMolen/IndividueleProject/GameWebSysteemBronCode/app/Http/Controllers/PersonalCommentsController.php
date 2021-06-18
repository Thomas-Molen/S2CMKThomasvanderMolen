<?php

namespace App\Http\Controllers;

use App\Helpers\TableReadabilityHelper;
use App\Repository\CommentRepository;

class PersonalCommentsController extends Controller
{
    public function index(TableReadabilityHelper $readabilityHelper, CommentRepository $commentRepository)
    {
        return view('pages.personal_comments')
            ->with(['comments' => $commentRepository->GetUserComment(), 'readabilityHelper' => $readabilityHelper]);
    }
}
