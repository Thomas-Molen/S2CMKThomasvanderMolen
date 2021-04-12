<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

/**
 * Class Run
 *
 * @property $id
 * @property $user_id
 * @property $duration
 * @property $created_at
 * @property $active
 *
 * @package App
 * @mixin \Illuminate\Database\Eloquent\Builder
 */
class Run extends Model
{
    protected $table = 'run';
    public $timestamps = false;

    static $rules = [
        'user_id' => 'required',
        'custom_name' => 'max:50'
    ];

    protected $perPage = 20;

    /**
     * Attributes that should be mass-assignable.
     *
     * @var array
     */
    protected $fillable = [
        'user_id',
        'active',
        'custom_name',
        'created_at'
    ];
}
