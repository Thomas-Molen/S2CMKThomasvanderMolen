<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

/**
 * Class Comment
 *
 * @property $id
 * @property $user_id
 * @property $run_id
 * @property $content
 * @property $created_at
 * @property $active
 *
 * @package App
 * @mixin \Illuminate\Database\Eloquent\Builder
 */
class Comment extends Model
{
    protected $table = 'comment';
    public $timestamps = false;

    static $rules = [
		'content' => 'required|max:500'
    ];

    protected $perPage = 1e20;

    /**
     * Attributes that should be mass-assignable.
     *
     * @var array
     */
    protected $fillable = [
        'user_id',
        'run_id',
        'content',
        'active',
        'created_at'
    ];

}
