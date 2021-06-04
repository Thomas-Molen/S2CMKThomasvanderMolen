<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

/**
 * Class Link
 *
 * @property $id
 * @property $name
 * @property $url
 * @property $run_id
 *
 * @package App
 * @mixin \Illuminate\Database\Eloquent\Builder
 */
class Link extends Model
{
    protected $table = 'link';
    public $timestamps = false;

    static $rules = [
		'url' => 'required | max:250',
        'name' => 'required | max:50'
    ];

    protected $perPage = 1e20;

    /**
     * Attributes that should be mass-assignable.
     *
     * @var array
     */
    protected $fillable = [
        'name',
        'url',
        'run_id',
        'user_id'
    ];

    public function user()
    {
        return $this->belongsTo(User::class);
    }

    public function run()
    {
        return $this->belongsTo(Run::class);
    }
}
