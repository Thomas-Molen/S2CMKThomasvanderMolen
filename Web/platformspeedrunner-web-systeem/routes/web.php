<?php

use App\Http\Controllers\Auth\LoginController;
use App\Http\Controllers\Auth\LogoutController;
use App\Http\Controllers\Auth\RegisterController;
use App\Http\Controllers\CommentController;
use App\Http\Controllers\PersonalRunsController;
use App\Http\Controllers\RoleController;
use App\Http\Controllers\RunController;
use App\Http\Controllers\UserController;
use App\Http\Controllers\LeaderboardController;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/
// Register
Route::get('/register', [RegisterController::class, 'index'])->name('register')->middleware('guest');
Route::post('/register', [RegisterController::class, 'store']);

// Login
Route::get('/login', [LoginController::class, 'index'])->name('login')->middleware('guest');
Route::post('/login', [LoginController::class, 'store']);

// Logout
Route::get('/logout', [LogoutController::class, 'store'])->name('logout');

// default accessible pages
Route::get('leaderboard', [LeaderboardController::class, 'index'])->name('leaderboard');
Route::get('/', [LeaderboardController::class, 'index'])->name('home');

//middleware
Route::group(['middleware' => ['auth']], function () {
    //user accessible pages
    Route::get('personal_runs', [PersonalRunsController::class, 'index'])->name('personal_runs');
    Route::get('create_comment/{run_id}', [CommentController::class, 'user_create'])->name('user_create_comment');

    //admin pages
    Route::resource('user', UserController::class);
    Route::resource('run', RunController::class);
    Route::resource('comment', CommentController::class);
    Route::resource('role', RoleController::class);
});
