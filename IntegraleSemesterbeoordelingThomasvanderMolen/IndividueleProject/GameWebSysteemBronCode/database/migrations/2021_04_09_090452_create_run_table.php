<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateRunTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('run', function (Blueprint $table) {
            $table->id();
            $table->foreignId('user_id')->nullable();
            $table->bigInteger('duration')->nullable();
            $table->dateTime('created_at')->nullable();
            $table->string('custom_name', 50)->nullable();
            $table->string('information', 5000)->nullable();
            $table->boolean('active')->default(1);
            $table->foreign('user_id')->references('id')->on('user');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('run');
    }
}
