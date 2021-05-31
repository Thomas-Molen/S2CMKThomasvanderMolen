<?php

namespace App\Http\Controllers;

use App\Helpers\QueryHelper;
use App\Helpers\TableReadabilityHelper;
use App\Models\Comment;
use App\Models\Run;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Http\Request;

class PersonalCommentsController extends Controller
{
    public function index(TableReadabilityHelper $readabilityHelper, QueryHelper $query)
    {
        return view('pages.personal_comments')
            ->with(['comments' => $query->GetUserComment(), 'readabilityHelper' => $readabilityHelper]);
    }
}
