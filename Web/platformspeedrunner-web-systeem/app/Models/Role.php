<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

/**
 * Class Role
 *
 * @property $id
 * @property $name
 * @property $description
 *
 * @package App
 * @mixin \Illuminate\Database\Eloquent\Builder
 */
class Role extends Model
{
    protected $table = 'role';
    public $timestamps = false;

    static $rules = [
		'name' => 'required|max:20',
        'description' => 'required|max:100',
    ];

    protected $perPage = 1e20;

    /**
     * Attributes that should be mass-assignable.
     *
     * @var array
     */
    protected $fillable = [
            'name',
            'description',
            'active'
        ];



}
