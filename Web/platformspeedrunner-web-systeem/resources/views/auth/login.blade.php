@extends('layouts.auth')
@section('title', 'Login')
@section('content')
    <div class="card">
        <div class="card-body login-card-body">

            @if (session('status'))
                <div style="color:red;">
                    <p class="login-box-msg">{{ session('status') }}</p>
                </div>
            @endif

            <form action="{{route('login')}}" method="post">
                @csrf
                <div class="input-group mb-3">
                    <input type="username" name="username" id="username" class="form-control" placeholder="Username" style="@error('username') border-color:red; @enderror">
                    <div class="input-group-append">
                        <div class="input-group-text" style="@error('username') border-color:red; @enderror">
                            <span class="fas fa-lock"></span>
                        </div>
                    </div>
                </div>
                @error ('username')
                <div style="color:red;margin-top: -15px;margin-bottom: 10px;">
                    {{ $message }}
                </div>
                @enderror
                <div class="input-group mb-3">
                    <input type="password" name="password" id="password" class="form-control" placeholder="Password" style="@error('password') border-color:red; @enderror">
                    <div class="input-group-append">
                        <div class="input-group-text" style="@error('password') border-color:red; @enderror">
                            <span class="fas fa-lock"></span>
                        </div>
                    </div>
                </div>
                @error ('password')
                <div style="color:red;margin-top: -15px;margin-bottom: 10px;">
                    {{ $message }}
                </div>
                @enderror
                <div class="row">
                    <div class="col-8">
                        <div class="icheck-primary">
                            <input type="checkbox" name="remember" id="remember">
                            <label for="remember">
                                Remember Me
                            </label>
                        </div>
                    </div>
                    <!-- /.col -->
                    <div class="col-4">
                        <button type="submit" class="btn btn-primary btn-block">Sign In</button>
                    </div>
                    <!-- /.col -->
                </div>
            </form>

        </div>
        <!-- /.login-card-body -->
    </div>
@endsection
