<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Foundation\Auth\User as Authenticatable;

/**
 * Class User
 *
 * @property $id
 * @property $username
 * @property $password
 * @property $unique_key
 *
 * @package App
 * @mixin \Illuminate\Database\Eloquent\Builder
 */
class User extends Authenticatable
{
    protected $table = 'user';
    public $timestamps = false;

    static $rules = [
		'username' => 'required|max:30',
        'password' => 'max:150',
        'unique_key' => 'required|max:20',
    ];

    protected $perPage = 1e20;

    /**
     * Attributes that should be mass-assignable.
     *
     * @var array
     */
    protected $fillable = [
        'username',
        'password',
        'unique_key',
        'active',
        'role_id'
    ];

    protected $hidden = [
        'password'
    ];

    public function comment()
    {
        return $this->hasMany(Comment::class, 'user_id', 'id');
    }
}
