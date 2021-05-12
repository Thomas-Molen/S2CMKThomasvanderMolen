<?php

use App\Http\Controllers\GameApiController;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

Route::middleware('auth:api')->get('/user', function (Request $request) {
    return $request->user();
});

Route::post('submit_run', [GameApiController::class, 'SubmitRun'])->name('submit_run_game');
Route::get('get_user', [GameApiController::class, 'GetUserData'])->name('get_user_game');
