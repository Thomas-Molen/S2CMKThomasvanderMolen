<?php


namespace App\Repository;


use App\Models\Link;

class LinkRepository
{
    public function Delete($id)
    {
        Link::find((int)$id)->delete();
    }
}
