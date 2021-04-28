@extends('layouts.auth')
@section('title', 'Register')
@section('content')
    <div class="card">
        <div class="card-body register-card-body">
            <p class="login-box-msg">Register</p>
            <form action="{{ route('post_register') }}" method="post">
                @csrf
                <div class="input-group mb-3">
                    <input type="text" name="username" id="username" class="form-control" placeholder="Username">
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fas fa-user"></span>
                        </div>
                    </div>
                </div>
                <div class="input-group mb-3">
                    <input name="password" id="password" type="password" class="form-control" placeholder="Password">
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fas fa-lock"></span>
                        </div>
                    </div>
                </div>
                <div class="input-group mb-3">
                    <input type="password" name="password_confirmation" id="password_confirmation" class="form-control" placeholder="Retype password">
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fas fa-lock"></span>
                        </div>
                    </div>
                </div>
                Unique Game Key
                <div class="input-group mb-3">
                    <input type="text" name="unique_key" id="unique_key" class="form-control" placeholder="Authentication Key" value="{{ $unique_key }}">
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fas fa-key"></span>
                        </div>
                    </div>
                </div>
                <!-- /.col -->
                <div class="col-4">
                    <button type="submit" class="btn btn-primary btn-block">Register</button>
                </div>
                <!-- /.col -->
            </form>

            <a href="{{ route('login') }}" class="text-center">Already have an account?</a>
        </div>
        <!-- /.form-box -->
    </div><!-- /.card -->
@endsection
