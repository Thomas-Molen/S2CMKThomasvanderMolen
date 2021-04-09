<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

/**
 * Class User
 *
 * @property $id
 * @property $username
 * @property $password
 * @property $unique_key
 * @property $upvotes
 *
 * @package App
 * @mixin \Illuminate\Database\Eloquent\Builder
 */
class User extends Model
{
    protected $table = 'user';
    public $timestamps = false;

    static $rules = [
		'username' => 'required|max:30',
        'password' => 'required|max:150',
    ];

    protected $perPage = 20;

    /**
     * Attributes that should be mass-assignable.
     *
     * @var array
     */
    protected $fillable = [
        'username',
        'password',
        'unique_key',
        'upvotes'
    ];

    protected $hidden = [
        'password'
    ];

}
